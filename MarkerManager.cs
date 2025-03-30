using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{

    // Class to store the positoin and rotation of the current marker
    public class Marker{
        public Vector3 position;
        public Quaternion rotation;

        public Marker(Vector2 pos, Quaternion rot){
            position = pos;
            rotation = rot;
        }
    }


    // List of markers for the tail to follow
    public List<Marker> markerList = new List<Marker>();

    // Constantly updating the list to add the current position and rotation to the list
    public void UpdateMarkerList(){
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

    // clearing the list for safety
    public void ClearMarkerList(){
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

    void FixedUpdate()
    {
        UpdateMarkerList();
    }
}
