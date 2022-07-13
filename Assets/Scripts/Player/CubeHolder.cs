using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CubeHolder : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] Transform StickMan;
    [SerializeField] Transform StackParent;

    List<GameObject> Cubes;
    private void Awake()
    {
        Cubes = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("YellowCube"))
        {

            if (Cubes.Contains(other.gameObject))
            {
                return;
            }

            Transform Cube = other.transform;
            Cube.gameObject.layer = LayerMask.NameToLayer("CubeStack");
            Cubes.Insert(0, other.gameObject);

            Cube.SetParent(StackParent);
            if (Cubes.Count > 1)
            {
                Cube.transform.localPosition = Cubes[1].transform.localPosition;
            }
            
            for (int i = 1; i < Cubes.Count; i++)
            {
                Cubes[i].transform.localPosition = Cubes[i].transform.localPosition + Vector3.up;
            }

            StickMan.position += Vector3.up;
            other.GetComponent<YellowCube>().SetCubeHolder(this);
        }
    }
    public async void CubesDecrese(GameObject yellowcube)
    {
        await Task.Delay(System.TimeSpan.FromSeconds(0.25));
              
        int index = Cubes.IndexOf(yellowcube);

        if (index == -1)
        {
            return;
        }

        Cubes.RemoveAt(index);

        for (int i = index; i < Cubes.Count; i++)
        {
            Cubes[i].transform.localPosition = Cubes[i].transform.localPosition + Vector3.down;
        }

        StickMan.position += Vector3.down;

        if (Cubes.Count == 0) 
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
        }
    }
}