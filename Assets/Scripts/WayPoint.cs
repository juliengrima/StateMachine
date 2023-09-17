using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    #region Champs
    [SerializeField] Transform[] _waypoints; // Tableau des points de passage
    [SerializeField] float _speed; // Vitesse de déplacement du GameObject
    //[SerializeField] float _distance;
 
    private int currentWaypointIndex = 0; // Index du point de passage actuel

    #endregion
    #region Unity LifeCycle
    // Start is called before the first frame update
    private void Reset()
    {
        _speed = 2f;
        //_distance = 0.5f;
    }
    void Awake()
    {
        
    }
    void Start()
    {
        // Assurez-vous que le tableau des waypoints n'est pas vide
        if (_waypoints.Length == 0)
        {
            Debug.LogError("Aucun waypoint n'a été assigné.");
            enabled = false; // Désactiver le script pour éviter des erreurs
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[currentWaypointIndex].position) < 0.1f)
        {
            // Passer au waypoint suivant
            currentWaypointIndex++;

            // Si nous avons atteint le dernier waypoint, revenir au premier
            if (currentWaypointIndex >= _waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // Déplacer le GameObject vers le waypoint actuel
        Vector3 targetPosition = _waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
    #endregion
    #region Methods
    void FixedUpdate ()
    {
        
    }
    void LateUpdate ()
    {
        
    }
    #endregion
    #region Coroutines
    #endregion
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class waypoint : MonoBehaviour
//{
//    enum Mode { Loop, PingPong }


//    [SerializeField] List<Transform> _points;
//    [SerializeField] int _destinationIndex;
//    [SerializeField] Mode _mode;


//    bool _reverse;

//    private void Update()
//    {
//        Transform dest = _points[_destinationIndex];

//        Vector3 direction = dest.position - transform.position;

//        direction.Normalize();
//        direction *= 0.1f;

//        // est-ce que l'on est arrivé
//        var distance = Vector3.Distance(dest.position, transform.position);
//        if (distance < 2f)
//        {
//            Debug.Log("arrivé");

//            if (_mode == Mode.Loop)
//            {
//                //Loop
//                _destinationIndex++;
//                if (_destinationIndex >= _points.Count)
//                {
//                    _destinationIndex = 0;
//                }
//            }
//            else if (_mode == Mode.PingPong)
//            {
//                // Ping-pong
//                if (_reverse == false)
//                {
//                    _destinationIndex++;
//                    if (_destinationIndex >= _points.Count)
//                    {
//                        _reverse = true;
//                        _destinationIndex--;
//                    }
//                }
//                else
//                {
//                    _destinationIndex--;
//                    if (_destinationIndex < 0)
//                    {
//                        _reverse = false;
//                        _destinationIndex++;
//                    }
//                }
//            }
//        }
//        else
//        {
//            transform.Translate(direction);
//        }

//    }

//}
