using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NanoMesh
{
    public class NanoSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject nanoPrefab;
        [SerializeField] private GameObject nanoParent;
        [SerializeField] private SkinnedMeshRenderer characterMesh;
        private List<Vector3> _sMNormals = new List<Vector3>();
        private List<Vector3> _sMVerts = new List<Vector3>();
        [SerializeField] private float offsetDistance;
        private List<GameObject> _nanoParts = new List<GameObject>();

        private void Start()
        {
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
            int batchSize = 100;
            int totalVerts = _sMVerts.Count;
            int totalNormals = _sMNormals.Count;

            for (int v = 0; v < totalVerts; v++)
            {
                Vector3 worldPos = transform.TransformPoint(_sMVerts[v]);
                GameObject nanoInstance = Instantiate(nanoPrefab, worldPos, Quaternion.identity);
                nanoInstance.transform.parent = nanoParent.transform;

                if (v % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            for (int i = 0; i < nanoParent.transform.childCount; i++)
            {
                _nanoParts.Add(nanoParent.transform.GetChild(i).gameObject);

                if (i % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            for (int i = 0; i < totalNormals; i++)
            {
                Vector3 normal = transform.TransformDirection(_sMNormals[i]);
                _nanoParts[i].transform.position = transform.TransformPoint(_sMVerts[i]) + normal * offsetDistance;

                if (i % batchSize == 0)
                {
                    yield return null;
                    ;
                }
            }

            yield return null;
        }
    }
}
