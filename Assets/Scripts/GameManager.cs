using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floorPrefab;
	public List<GameObject> floors;

	public List<GameObject> obstaclePrefabs;

	public float scrollSpeed = 7f;
	public int floorRenderAmount = 10;

	public float floorSize = 54.5000063f;
	public float laneSize = 1.75f;


	public GameObject player;

	bool stopped = false;

	void Start()
	{
		// loop for number of floors that need to render
		for (int i = 0; i < floorRenderAmount; i++)
		{
			// make new floor
			GenerateChunk();
		}
	}

	void Update()
	{
		if(!stopped) { 
			for (int i = 0; i < floors.Count; i++)
			{
				// get current floor
				GameObject floor = floors[i];

				// move floor backwards
				floor.transform.position += Vector3.back * scrollSpeed * Time.deltaTime;

				// checks if floor is far back enough to be destroyed
				if(floor.transform.position.z < -floorSize)
				{
					// destroy and remove floor
					floors.Remove(floor);
					Destroy(floor);

					// make new floor
					GenerateChunk();

					// resets i to make up for deleted floor
					i--;
				}
			}
		}
	}

	GameObject GenerateChunk()
	{
		// make new floor
		GameObject newFloor = Instantiate(floorPrefab);
		floors.Add(newFloor);

		// checks if this is the only floor
		if (floors.Count == 1)
		{
			// place new floor at the beginning
			newFloor.transform.position = new Vector3(0, 0, 0);
		}
		else
		{
			// place new floor in front of the last floor
			newFloor.transform.position = new Vector3(0, 0, floors[floors.Count - 2].transform.position.z + floorSize);
		}

		//SpawnObstacles(newFloor.transform);

		return newFloor;
	}

	void SpawnObstacles(Transform floor)
	{
		// Define the number of obstacles you want to spawn.
		int numberOfObstacles = Random.Range(4, 15);

		// Spawn the specified number of obstacles at random positions.
		for (int i = 0; i < numberOfObstacles; i++)
		{
			// generate new obstacle
			GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)], floor, true);

			obstacle.transform.parent = floor;

			// place obstacle in floor
			obstacle.transform.position = new Vector3(Random.Range(-1, 2) * laneSize, obstacle.transform.position.y, Random.Range(floor.transform.position.z - (floorSize / 2f), floor.transform.position.z + (floorSize / 2f)));
		}
	}

	public void Stop()
	{
		stopped = true;
		player.GetComponent<Animator>().SetBool("death", true);
	}
}