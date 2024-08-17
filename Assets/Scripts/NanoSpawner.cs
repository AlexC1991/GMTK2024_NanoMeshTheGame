using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NanoMesh
{
    public class NanoSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject locationParent;
        [SerializeField] private GameObject nanoPrefab;
        [SerializeField] private GameObject nanoParent;
        [SerializeField] private SkinnedMeshRenderer characterMesh;
        private List<Vector3> _sMNormals = new List<Vector3>();
        private List<Vector3> _sMVerts = new List<Vector3>();
        [SerializeField] private float offsetDistance;

        private void Start()
        {
            /*if (locationParent == null)
            {
                Debug.LogError("Location parent is not set!");
                return;
            }

            foreach (Transform child in locationParent.transform)
            {
                _locationSpots.Add(child.gameObject);
            }*/
            
            if (characterMesh == null)
            {
                Debug.LogError("Character mesh is not set!");
                return;
            }
            
            foreach (var m in characterMesh.sharedMesh.normals)
            {
                _sMNormals.Add(m);
            }
            
            foreach (var m in characterMesh.sharedMesh.vertices)
            {
                _sMVerts.Add(m);
            }

            StartCoroutine(AssignGameObjectToLocations());
        }

        private IEnumerator AssignGameObjectToLocations()
        {
            /*foreach (var g in _locationSpots)
            {
                GameObject parentG = Instantiate(nanoPrefab, g.transform.position, Quaternion.identity);
                parentG.transform.parent = nanoParent.transform;
            }*/

            foreach (var v in _sMVerts)
            {
                for (int i = 0; i < _sMNormals.Count; i++)
                {
                    Vector3 normal = transform.TransformDirection(_sMNormals[i]);
                    Vector3 worldPos = transform.TransformPoint(v);
                    GameObject nanoInstance = Instantiate(nanoPrefab, worldPos + normal * offsetDistance, Quaternion.LookRotation(normal));
                    nanoInstance.transform.parent = nanoParent.transform;
                }
            }
            yield return null;
        }
    }
}
