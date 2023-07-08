using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YJL.EditorHelper.Editor
{
    [CustomPropertyDrawer(typeof(DerivedAttribute))]
    public class DerivedAttributeDrawer : PropertyDrawer
    {
        private bool showCreateField = false;
        private int typeIndex = 0;
        private ScriptableObject so;
        private string[] typeStrings = new string[] {};
        private List<Type> types = new List<Type>();
        private string objectName = "Default";
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float originalHeight = base.GetPropertyHeight(property, label);
            return showCreateField ? originalHeight + EditorGUIUtility.singleLineHeight * 2 : originalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            float space = 15.0f;
            float oneUWidth = 30.0f;
            float halfUWidth = 15f;
            float fullWidth = position.width;
            float halfWidth = fullWidth / 2;
            float quaterWidth = fullWidth / 4;
            
            Rect foldoutRect = new Rect(position);
            foldoutRect.width = halfUWidth;
            foldoutRect.height = EditorGUIUtility.singleLineHeight;
            Rect propertyRect = new Rect(position);
            propertyRect.height = EditorGUIUtility.singleLineHeight;
            propertyRect.width = fullWidth - halfUWidth;
            propertyRect.x = halfUWidth;
            Rect popupField = new Rect(propertyRect);
            popupField.height = EditorGUIUtility.singleLineHeight;
            popupField.y += EditorGUIUtility.singleLineHeight;
            popupField.width = propertyRect.width - oneUWidth - space;
            Rect refreshRect = new Rect(popupField);
            refreshRect.x = popupField.x + popupField.width + space;
            refreshRect.width = oneUWidth;
            Rect nameField = new Rect(propertyRect);
            nameField.width = halfWidth - space;
            nameField.height = EditorGUIUtility.singleLineHeight;
            nameField.y += EditorGUIUtility.singleLineHeight * 2;
            Rect createField = new Rect(propertyRect);
            createField.width = quaterWidth - space;
            createField.height = EditorGUIUtility.singleLineHeight;
            createField.x = nameField.x + nameField.width + space;
            createField.y += EditorGUIUtility.singleLineHeight * 2;
            Rect deleteField = new Rect(createField);
            deleteField.height = EditorGUIUtility.singleLineHeight;
            deleteField.x = createField.x + createField.width + space;

            showCreateField = EditorGUI.Foldout(foldoutRect, showCreateField, GUIContent.none);
            EditorGUI.PropertyField(propertyRect, property, label);
            if (showCreateField)
            {

                if (GUI.Button(refreshRect, "⟲"))
                {
                    var fi = GetFieldInfo(property);
                    types = FindAllDerivedTypes(GetType(property));
                    typeStrings = types.Select(v => v.ToString()).ToArray();
                }

                typeIndex = EditorGUI.Popup(popupField, "Type", typeIndex, typeStrings);
                if (property.objectReferenceValue != null)
                {
                    objectName = property.objectReferenceValue.name;
                }

                objectName = EditorGUI.TextArea(nameField, objectName);
                if (property.objectReferenceValue != null)
                {
                    property.objectReferenceValue.name = objectName;
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }

                if (GUI.Button(createField, "CreateObject"))
                {
                    so = ScriptableObject.CreateInstance(types[typeIndex]);
                    so.name = String.IsNullOrEmpty(objectName) ? types[typeIndex].ToString().Split(".").Last() : objectName;
                    AssetDatabase.AddObjectToAsset(so, property.serializedObject.targetObject);
                    AssetDatabase.SaveAssets();
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                    property.objectReferenceValue = so;
                }

                if (GUI.Button(deleteField, "Delete"))
                {
                    if (property.objectReferenceValue != null)
                    {
                        Undo.DestroyObjectImmediate(property.objectReferenceValue);
                    }

                    AssetDatabase.SaveAssets();
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }
            EditorGUI.EndProperty();
        }

        private List<Type> FindAllDerivedTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t))
                .ToList();
        }

        public FieldInfo GetFieldInfo(SerializedProperty property)
        {
            System.Type parentObjectType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = GetFieldViaPath(parentObjectType, property.propertyPath);
            return fi;
        }
        
        public System.Type GetType(SerializedProperty property)
        {
            System.Type parentObjectType = property.serializedObject.targetObject.GetType();
            System.Reflection.FieldInfo fi = GetFieldViaPath(parentObjectType, property.propertyPath);
            Type type = fi.FieldType;
            if (fi.FieldType.IsArray)
            {
                type = type.GetElementType();
            }

            if (fi.FieldType.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            return type;
        }
        
        public System.Reflection.FieldInfo GetFieldViaPath(System.Type type,string path)
        {
            System.Type parentType = type;
            System.Reflection.FieldInfo fi = type.GetField(path);
            string[] splits = path.Split(".");
            for (int i = 0; i < splits.Length; i++)
            {
                fi = parentType.GetField(splits[i]);
                if (fi != null)
                {
                    if (fi.FieldType.IsArray)
                    {
                        parentType = fi.FieldType.GetElementType();
                        i += 2;
                        continue;
                    }

                    if (fi.FieldType.IsGenericType)
                    {
                        parentType = fi.FieldType.GetGenericArguments()[0];
                        i += 2;
                        continue;
                    }
                    parentType = fi.FieldType;
                }
                else
                {
                    break;
                }

            }

            return fi;

        }
    }
}