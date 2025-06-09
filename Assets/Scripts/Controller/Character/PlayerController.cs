using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


// Con esto, nos aseguramos de que al objeto al que le implementamos el script tenga estos objetos, si no los tiene, los crea
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    //Variables del jugador
    public static PlayerController Instance { get; private set; }
    private PlayerModel model = new PlayerModel();
    private PlayerView view;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    private bool isUpMoving, isDownMoving, isLeftMoving, isRightMoving;

    // Nos encargamos de instaciar un objeto de tipo PlayerController cuando se ejecute una escena
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Funcion que se asegura de que si hay un cambio de escena, obtener el view e inicializarla
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (view == null)
            view = GetComponent<PlayerView>();

        if (view != null)
            view.InitializeView();
        else
            Debug.LogError("PlayerView no se encontró");
    }
    // Obtenemos los elementos al iniciar la escena
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PlayerView>();
        transform.position = model.SpawnPosition; // Cambiamos la posicion del jugador a la asignada mediante una variable
    }

    // Poniendo estas funciones en update nos aseguramos de que se ejecutan por cada frame, con esto conseguimos el movimiento del jugador
    void Update()
    {
        if (Time.timeScale == 0) return; // Si el juego está parado, no entra en el update

        moveInput = Vector2.zero; // Esto lo usamos en caso de que dejemos de mover al jugador que no quede ningun residuo
        HandlePlayerMovement(); // Funcion que maneja el movimiento del jugador en base a los btn
        view.ShowTileSelector(GetTargetCell()); // Actualizamos la posicion del selector
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        float speed = model.IsRunning ? model.MoveRunningSpeed : model.MoveSpeed; // Nos aseguramos si el jugador está corriendo o no y dependiendo de eso tiene una velocidad u otra
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime); // Movemos al jugador 
    }

    private void HandlePlayerMovement()
    {
        // Dependiendo de las entradas de los btn cambiamos el movimiento del jugador
        if (isUpMoving) moveInput.y += 1;
        if (isDownMoving) moveInput.y -= 1;
        if (isRightMoving) moveInput.x += 1;
        if (isLeftMoving) moveInput.x -= 1;

        if (moveInput != Vector2.zero)
        {
            moveInput.Normalize(); // Con esto nos aseguramos de que si va en vertical no aumente la velocidad y sea constante
            model.LastPosition = moveInput; // Guardamos la ultima posicion del jugador
        }

        view.UpdateAnimation(moveInput, model.IsRunning); // Cambio de animación dependiendo de las variables anteriores
    }

    //Funcion que cambia los valores de los booleanos
    public void PlayerMove(string direction, bool isPressed)
    {
        switch (direction)
        {
            case "up": isUpMoving = isPressed; break;
            case "down": isDownMoving = isPressed; break;
            case "left": isLeftMoving = isPressed; break;
            case "right": isRightMoving = isPressed; break;
        }
    }

    // Funcion que setea el bool en caso de que el jugador esté o no corriendo
    public void SetRunningState(bool isRunning)
    {
        model.IsRunning = isRunning;
    }

    // Obtenemos la celda en la que está mirando el jugador
    public Vector3Int GetTargetCell()
    {
        // Obtenemos la direccion del jugador
        Vector2 dir = (model.LastPosition == Vector2.zero) ? Vector2.down : model.LastPosition;

        // Convertir la direccion en Vector3
        Vector3Int direction = new Vector3Int(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y), 0);

        // Obtenemos la celda actual del jugador
        if (view.referenceTileMap != null)
        {
            Vector3Int playerCell = view.referenceTileMap.WorldToCell(transform.position);
            return playerCell + direction;
        }
        return Vector3Int.zero;
    }

    // Si el jugador entra en contacto con un objeto con tag 'seed', llamamos a una funcion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Seeds"))
        {
            GameManager.Instance.OnSeedCollision(other.gameObject);
        }
    }

    // Si el jugador colisiona con alguno de los siguientes tags, hace sus respectivas funciones, esta funcion se ha hecho para el cambio de escenas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            // Habrá un objeto con el Tag SceneShop y otro con SceneHome
            case "SceneShop":
                SceneManager.LoadScene("ShopCaravanScene");
                transform.position = Vector2.zero; // Cambiamos la posicion del jugador
                ResetMovement(); // Reseteamos el movimiento para que no inicie andando porque hayan residuos
                break;

            case "SceneHome":
                SceneManager.LoadScene("HomeScene");
                transform.position = new Vector2(25, 3); // Cambiamos la posicion del jugador
                ResetMovement(); // Reseteamos el movimiento para que no inicie andando porque hayan residuos
                break;
        }
    }

    // Funcion para resetear todos los bools de movimiento a falso
    public void ResetMovement()
    {
        isUpMoving = false;
        isDownMoving = false;
        isLeftMoving = false;
        isRightMoving = false;
    }

    // Obtenemos el dinero del jugador
    public int GetMoney()
    {
        return model.PlayerMoney;
    }

    // Seteamos el dinero del jugador a la cantidad pasada
    public void SetMoney(int money)
    {
        model.PlayerMoney = money;
    }

}
