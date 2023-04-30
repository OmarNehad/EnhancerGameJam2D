using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private Tilemap tilemap;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 startingPos;
    private SpriteRenderer BuildingSpriteRend;

    public int demaningScore = 10;
    public Text demaningScoreText;
    private AudioSource audioSource;

//    public int redScore = 0;

    public int count = 0;
    public Text countText;

    public int AdditionalCountMinus;
    public Text additionalCountText;

    public int greenScore = 0;

    //public int blueScore = 0;
    //public GameObject UIPart;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        demaningScoreText.text = demaningScore.ToString();
        startingPos = transform.position;
        BuildingSpriteRend = GetComponent<SpriteRenderer>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        countText.text = count.ToString();
    }


    private void Update()
    {

        if(count == 0) return;
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Cast a ray from the mouse position and check for collisions with sprites
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // If a sprite was clicked, start dragging it
                isDragging = true;
                offset = hit.transform.position - mousePosition;
            }


        }


        if (Input.GetMouseButtonUp(0))
        {

            if (isDragging)
            {
                isDragging = false;

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);
                gridPosition.z = 0;

                if (tilemap.HasTile(gridPosition))
                {
                    //Tile newTile = new Tile(); // create a new Tile
                    //newTile.sprite = BuildingSprite; // set the sprite of the new Tile to the new sprite you want to use
                    //tilemap.SetTile(gridPosition, newTile); // set the new Tile at the position of the tile you want to change

                    Tile oldTile = (tilemap.GetTile(gridPosition) as Tile);
                    if (oldTile.name == "taken")
                    {
                        transform.position = startingPos;
                        return;
                            };
                    Tile newTile = Instantiate(oldTile);
                    newTile.sprite = BuildingSpriteRend.sprite;
                    newTile.name = "taken";
                    tilemap.SetTile(gridPosition, newTile);
                    audioSource.Play();

                    //if (clickedTile.sprite.name == "Water_Back") { transform.position = startingPos; return; }

                    //clickedTile.sprite = BuildingSpriteRend.sprite;

                    tilemap.RefreshTile(gridPosition);

                    //Update Game scores

                    GameManager.Instance.ChangeDemandingValue(demaningScore);
                    //GameManager.Instance.ChangeRedValue(redScore);
                    GameManager.Instance.ChangeGreenValue(greenScore);
                    //GameManager.Instance.ChangeBlueValue(blueScore);
                    count -= 1;
                    countText.text = count.ToString();
                    if(additionalCountText != null)
                    {
                        int currentCount = int.Parse(additionalCountText.text);
                        currentCount -= AdditionalCountMinus;
                        additionalCountText.text = currentCount.ToString();

                        //int currentCount = int.Parse(additionalCountText.text);
                    }


                }

                transform.position = startingPos;
            }

        }





        if (isDragging)
        {
            // Update the position of the sprite while it's being dragged
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            //transform.position = mousePosition;

        }


    }

    //Vector3Int[] neighborOffsets = new Vector3Int[]
    //    {
    //        new Vector3Int(1, 0, 0),
    //        new Vector3Int(-1, 0, 0),
    //        new Vector3Int(0, 1, 0),
    //        new Vector3Int(0, -1, 0)
    //    };


    //public enum Size
    //{
    //    Small = 0,
    //    Medium = 1,
    //    Large = 2,

    //}

    //public Size size;
    //private void Start()
    //{
    //    startingPos = transform.position;
    //}


    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        // Get the mouse position in world space
    //        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //        // Cast a ray from the mouse position and check for collisions with sprites
    //        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
    //        if (hit.collider != null)
    //        {

    //            // If a sprite was clicked, start dragging it
    //            isDragging = true;
    //            offset = hit.transform.position - mousePosition;
    //        }

    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isDragging = false;

    //        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);
    //        gridPosition.z = 0;
    //        Debug.Log(gridPosition);

    //        if (tilemap.HasTile(gridPosition))
    //        {
    //            tilemap.transform.position = (tilemap.GetCellCenterWorld(gridPosition) - tilemap.GetCellCenterWorld(gridPosition - Vector3Int.one)) / 2;
    //            startingPos = transform.position;
    //        }
    //        else
    //        {
    //            transform.position = startingPos;
    //            startingPos = transform.position;

    //        }

    //    }
    //    if (isDragging)
    //    {
    //        // Update the position of the sprite while it's being dragged
    //        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    //    }
    //}


    //public void FindNeighboringTiles(Vector3Int startCell)
    //    {


    //    for (int i = 0; i<neighborOffsets.Length; i++)
    //    {
    //        Vector3Int neighborCell = startCell + neighborOffsets[i];
    //        TileBase neighborTile = tilemap.GetTile(neighborCell);

    //        if (neighborTile != null)
    //        {
    //            //return neighborTile.GetCellCenterLocal();
    //        }
    //    }
    //    }

}