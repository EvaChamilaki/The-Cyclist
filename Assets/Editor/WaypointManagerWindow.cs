using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]

    public static void Open() {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("WaypointRoot Empty", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons() {
        if (GUILayout.Button("Create Waypoint")) {
            CreateWaypoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>()) {
            if (GUILayout.Button("Add Branch Waypoint")) {
                CreateBranch();
            }
            if (GUILayout.Button("Create Waypoint Before")) {
                CreateWaypointBefore();
            }
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if (GUILayout.Button("Remove Waypoint")) {
                RemoveWaypoint();
            }
        }
    }

    void CreateWaypoint() {
        GameObject waypointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1) {
            waypoint.prevWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.prevWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = waypoint.prevWaypoint.transform.position;
            waypoint.transform.forward = waypoint.prevWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }

    void CreateWaypointBefore() {
        GameObject waypointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.prevWaypoint != null) {
            newWaypoint.prevWaypoint = selectedWaypoint.prevWaypoint;
            selectedWaypoint.prevWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.prevWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void CreateWaypointAfter() {
        GameObject waypointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObj.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObj.GetComponent<Waypoint>();
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObj.transform.position = selectedWaypoint.transform.position;
        waypointObj.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.prevWaypoint = selectedWaypoint;

        if (selectedWaypoint.nextWaypoint != null) {
            selectedWaypoint.nextWaypoint.prevWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoveWaypoint() {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if (selectedWaypoint.nextWaypoint != null) {
            selectedWaypoint.nextWaypoint.prevWaypoint = selectedWaypoint.prevWaypoint;
        }
        if (selectedWaypoint.prevWaypoint != null)
        {
            selectedWaypoint.prevWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.prevWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CreateBranch() { 
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint branchedForm = Selection.activeGameObject.GetComponent<Waypoint>();
        branchedForm.branches.Add(waypoint);

        waypoint.transform.position = branchedForm.transform.position;
        waypoint.transform.forward = branchedForm.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }

}
