using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    //temp fun variables
    [HideInInspector]
    public int generateAmount = 0;

    [SerializeField]
    private GameObject floor = null;
    struct Room
    {
        public Vector2 size;
        public Vector2 pos;
    }

    List<Room> rooms;

    [SerializeField]
    private GameObject levelstart = null;


    private int[,] levelBaseData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int minRooms = 3;
    private int maxRooms = 6;

    private int minRoomSize = 4;
    private int maxRoomSize = 7;//these variables let us change generation functionality easily

    /*
     * working on a linear generation
     */
    public void generateLevel()
    {
        generateAmount++;
        for(int i = levelstart.transform.childCount; i > 0; i--)
        {
            GameObject.Destroy(levelstart.transform.GetChild(i-1).gameObject);
        }

        levelBaseData = new int[25, 25];

        levelBaseData[12, 0] = 1;//defining the start position

        int roomCount = Random.Range(minRooms,maxRooms);//get a count for how many rooms to make

        rooms = new List<Room>();

        for (int i = 0; i < roomCount; i++){
            createRoom();
        }

        for (int i = 0; i < levelBaseData.GetLength(0); i++)
        {
            for (int n = 0; n < levelBaseData.GetLength(1); n++)
            {
                if (levelBaseData[i, n] == 1)
                {
                    GameObject newFloor =  GameObject.Instantiate(floor);
                    newFloor.transform.parent = levelstart.transform;
                    newFloor.transform.position = new Vector3(i*4,0,n*4) + levelstart.transform.position;
                    newFloor.transform.localScale = new Vector3(200, 200, 200);
                    newFloor.layer = LayerMask.NameToLayer("Ground");
                }
            }
        }


    }
    private void createRoom()
    {
        Room tempRoom = new Room();
        tempRoom.size = new Vector2(Random.Range(minRoomSize,maxRoomSize),Random.Range(minRoomSize, maxRoomSize));
        if (rooms.Count == 0)
        {
            tempRoom.pos = new Vector2(0, 0);//position is the bottom left corner of the room
        }
        else
        {
            tempRoom.pos = new Vector2(Random.Range(0, levelBaseData.GetLength(0) - tempRoom.size.x), Random.Range(0, levelBaseData.GetLength(1) - tempRoom.size.y));
        }
        for(int i = 0; i < tempRoom.size.x; i++)
        {
            for (int n = 0; n < tempRoom.size.y; n++)
            {
                levelBaseData[(int)tempRoom.pos.x + i, (int)tempRoom.pos.y + n] = 1;
            }
        }
        rooms.Add(tempRoom);
    }
    private void createConnector()
    {

    }
}
