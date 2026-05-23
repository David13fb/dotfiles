/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using UCM.IAV.Movimiento;

namespace UCM.IAV.Navegacion
{

    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Abstract class for graphs
    /// </summary>
    public abstract class Graph : MonoBehaviour
    {
        // Aquí el grafo entero es representado con estas listas, que luego puede aprovechar el algoritmo A*.
        // El pseudocódigo de Millington no asume que tengamos toda la información del grafo representada y por eso va guardando registros de los nodos que visita... pero si nos es posible, OPCIONALMENTE podemos usar estas variables como una CACHÉ donde tener toda la información
        public GameObject vertexPrefab;
        protected List<Vertex> vertices;
        protected List<List<Vertex>> neighbourVertex;
        protected List<List<float>> costs;
        protected bool[,] mapVertices;
        protected float[,] costsVertices; // Costes reales (g)... aunque también se podría crear una clase para las conexiones y poner los costes ahí, como en el pseudocódigo de Millington. Esto está 'optimizado' porque sabemos que trabajamos con una rejilla...
        protected int numCols, numRows;
        public float cellSize = 1f;
        
        // Esto de la heurística es para algoritmos de búsqueda con estrategias informadas como A*, naturalmente.
        // Un delegado especifica la cabecera de una función, la que sea, que cumpla con esos parámetros y devuelva ese tipo.
        // Cuidado al implementarlas, porque no puede ser que la distancia -por ejemplo- entre dos casillas tenga una heurística más cara que el coste real de navegar de una a otra.
        public delegate float Heuristic(Vertex a, Vertex b);

        // Used for getting path in frames
        public List<Vertex> path;


        public virtual void Start()
        {
            Load();
        }

        public virtual void Load() { }

        public virtual int GetSize()
        {
            if (ReferenceEquals(vertices, null))
                return 0;
            return vertices.Count;
        }

        public virtual void UpdateVertexCost(Vector3 position, float costMultipliyer) { }

        public virtual Vertex GetNearestVertex(Vector3 position)
        {
            //Get the round cell base in the position and the cellSize
            int fil = (int)Math.Round(position.x / cellSize);
            int col = (int)Math.Round(position.z / cellSize);

            //Get the Vertex Id 
            int idVertex = Math.Max(numRows, numCols) * col + fil; 
            return vertices[idVertex];
        }

        public virtual GameObject GetRandomPos()
        {
            return null;
        }

        public virtual Vertex[] GetNeighbours(Vertex v)
        {
            if (ReferenceEquals(neighbourVertex, null) || neighbourVertex.Count == 0 ||
                v.id < 0 || v.id >= neighbourVertex.Count)
                return new Vertex[0];
            return neighbourVertex[v.id].ToArray();
        }

        public virtual float[] GetNeighboursCosts(Vertex v)
        {
            if (ReferenceEquals(neighbourVertex, null) || neighbourVertex.Count == 0 ||
                v.id < 0 || v.id >= neighbourVertex.Count)
                return new float[0];

            Vertex[] neighs = neighbourVertex[v.id].ToArray();
            float[] costsV = new float[neighs.Length];
            for (int neighbour = 0; neighbour < neighs.Length; neighbour++) {
                int j = (int)Mathf.Floor(neighs[neighbour].id / numCols);
                int i = (int)Mathf.Floor(neighs[neighbour].id % numCols);
                costsV[neighbour] = costsVertices[j, i];
            }

            return costsV;
        }

        // Encuentra caminos óptimos
        public List<Vertex> GetPathBFS(GameObject srcO, GameObject dstO)
        {
            // IMPLEMENTAR ALGORITMO BFS
            return new List<Vertex>();
        }

        // No encuentra caminos óptimos
        public List<Vertex> GetPathDFS(GameObject srcO, GameObject dstO)
        {
            // IMPLEMENTAR ALGORITMO DFS
            return new List<Vertex>();
        }

        public List<Vertex> GetPathAstar(GameObject srcO, GameObject dstO, Heuristic h = null)
        {
            //Initial and last Vertex of the Graph    
            Vertex start = GetNearestVertex(srcO.transform.position);
            Vertex end = GetNearestVertex(dstO.transform.position);

            //Queue and Dictionaries when we save the Vertex
            BinaryHeap<Vertex> openSet = new BinaryHeap<Vertex>();
            Dictionary<Vertex, float> gScore = new Dictionary<Vertex, float>();
            Dictionary<Vertex, Vertex> fathers = new Dictionary<Vertex, Vertex>();

            foreach (Vertex v in this.vertices)
            {
                gScore[v] = float.PositiveInfinity;
                fathers[v] = null;
            }

            //Initialize the first cost
            gScore[start] = 0;
            //Set the heuristic cost
            float finit = h(start, end);

            //Set the initial Vertex
            openSet.Add(start);
            //While to search the path
            while (openSet.Count > 0)
            {
                Vertex actual = openSet.Remove();

                  if (actual == end)
                {
                    List<Vertex> path = new List<Vertex>();
                    Vertex current = actual;

                    while (current!=null)
                    {
                        path.Add(current);
                        current = fathers[current];
                    }
                    return path;
                }
                // neighbours
                Vertex[] neighbours = GetNeighbours(actual);
                float[] costes = GetNeighboursCosts(actual);
                
                //Search for each neighbour
                for (int i = 0; i < neighbours.Length; i++)
                {
                    
                    Vertex neighbour = neighbours[i];
                    float gTentativo = gScore[actual] + costes[i];
                    if (gTentativo < gScore[neighbour])
                    {
                        
                        fathers[neighbour] = actual;
                        gScore[neighbour] = gTentativo;

                        float fScore = gTentativo + h(neighbour, end);
                        neighbour.cost = fScore;
                        openSet.Add(neighbour);
                    }
                }
            }
            
            return new List<Vertex>();
        }


        public List<Vertex> Smooth(List<Vertex> inputPath)
        {
            // IMPLEMENTAR SUAVIZADO DE CAMINOS

            List<Vertex> outputPath = new List<Vertex>();
            for (int i = inputPath.Count-1; i > inputPath.Count - GameManager.instance.getNumSmoothPath() - 1; i--)
            {
                outputPath.Add(inputPath[i]);
            }
            return outputPath; 
        }

        // Reconstruir el camino, dando la vuelta a la lista de nodos 'padres' /previos que hemos ido anotando
        private List<Vertex> BuildPath(int srcId, int dstId, ref int[] prevList)
        {
            List<Vertex> path = new List<Vertex>();

            if (dstId < 0 || dstId >= vertices.Count) 
                return path;

            int prev = dstId;
            do
            {
                path.Add(vertices[prev]);
                prev = prevList[prev];
            } while (prev != srcId);
            return path;
        }
        public static float HeuristicEuclidean(Vertex a, Vertex b)
        {
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;
            return Mathf.Sqrt(Mathf.Pow(bTransform.position.x - aTransform.position.x, 2) + Mathf.Pow(bTransform.position.z - aTransform.position.z, 2));
        }
        
        public static float HeuristicManhattan(Vertex a, Vertex b)
        {
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;
            return Mathf.Abs(aTransform.position.x - bTransform.position.x) + Mathf.Abs(aTransform.position.z - bTransform.position.z);
        }
    }
}
