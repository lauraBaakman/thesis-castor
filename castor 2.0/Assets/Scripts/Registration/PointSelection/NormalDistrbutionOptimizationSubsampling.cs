using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Registration
{
    /// <summary>
    /// Normal distribution optimization subsampling as proposed by Rusinkiewicz,
    /// S., & Levoy, M. (2001). Efficient variants of the ICP algorithm. In 3-D
    /// Digital Imaging and Modeling, 2001. Proceedings. Third International
    /// Conference on (pp. 145-152). IEEE..
    /// </summary>
    public class NormalDistributionOptimizationSubsampling : IPointSampler
    {
        public List<Point> Sample(SamplingInformation samplingInfo)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Bin the normals with the approach discussed here: 
    /// https://stackoverflow.com/a/18319848/1357229
    /// </summary>
    public class NormalBinner
    {
        private readonly UniformPolyhedron polyhedron;

        private static Dictionary<int, UniformPolyhedron> polyhedra = new Dictionary<int, UniformPolyhedron>
        {
            {04, new TetraHedron()},
            {06, new Cube()},
            {08, new OctaHedron()},
            {12, new Dodecahedron()},
            {20, new IsocaHedron()}
        };

        public NormalBinner(int numberOfBins)
        {
            if (!polyhedra.TryGetValue(numberOfBins, out polyhedron)) throw new ArgumentException(string.Format("NormalDistribution Subsampling with {0} is not supported.", numberOfBins));
        }

        public NormalBinner(UniformPolyhedron polyhedron)
        {
            this.polyhedron = polyhedron;
        }

        /// <summary>
        /// Interface for the uniform polyhedra that we'll use to subdivide the
        /// unit sphere, as proposed by https://stackoverflow.com/a/18319848/1357229
        /// </summary>
        public abstract class UniformPolyhedron
        {
            public readonly Vector3[] normals;

            protected UniformPolyhedron(Vector3[][] faces)
            {
                this.normals = ComputeNormals(faces);
            }

            public virtual int GetNearestNormalIdx(Vector3 normal)
            {
                return ArgMaxDot(normal, this.normals);
            }

            protected int ArgMaxDot(Vector3 normal, Vector3[] normals)
            {
                float dot, maxDot = float.MinValue;
                int maxIdx = -1;

                for (int N = normals.Count(), i = 0; i < N; i++)
                {
                    dot = Vector3.Dot(normal, normals[i]);
                    if (dot > maxDot)
                    {
                        maxIdx = i;
                        maxDot = dot;
                    }
                }
                return maxIdx;
            }

            private Vector3[] ComputeNormals(Vector3[][] faces)
            {
                Vector3[] faceNormals = new Vector3[faces.Count()];

                Vector3[] faceVertices;
                for (int N = faces.Count(), i = 0; i < N; i++)
                {
                    faceVertices = faces[i];
                    faceNormals[i] = Utils.NumericalMath.NewellsMethod(faceVertices);
                }
                return faceNormals;
            }
        }

        internal class TetraHedron : UniformPolyhedron
        {
            // Mathematica: CForm[PolyhedronData["Tetrahedron", "Faces", "Polygon"]]
            private static readonly Vector3[][] faces = {
                new Vector3[]{
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3)), -0.5f ,-1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3)), +0.5f ,-1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(1/Mathf.Sqrt(3),           +0.0f ,-1/(2.0f * Mathf.Sqrt(6))),
                },
                new Vector3[]{
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3)), +0.5f, -1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3)), -0.5f, -1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(0                        , +0.0f, Mathf.Sqrt(0.6666666666666666f) - 1/(2.0f * Mathf.Sqrt(6))),
                },
                new Vector3[]{
                    new Vector3(1/Mathf.Sqrt(3),           +0.0f, -1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(0.0f,                      +0.0f, Mathf.Sqrt(0.6666666666666666f) - 1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3)), -0.5f, -1/(2.0f * Mathf.Sqrt(6))),
                },
                new Vector3[]{
                    new Vector3(0.0f,                         0.0f, Mathf.Sqrt(0.6666666666666666f) - 1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(1/Mathf.Sqrt(3.0f),           0.0f, -1/(2.0f * Mathf.Sqrt(6))),
                    new Vector3(-1/(2.0f * Mathf.Sqrt(3.0f)), 0.5f, -1/(2.0f * Mathf.Sqrt(6)))
                },
            };

            internal TetraHedron()
                : base(faces)
            { }
        }

        internal class Cube : UniformPolyhedron
        {
            //Mathematica: PolyhedronData["Cube", "Faces", "Polygon"]
            private static readonly Vector3[][] faces = {
                    new Vector3[] {
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, -0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f)
                    },
                    new Vector3[] {
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f)
                    },
                    new Vector3[] {
                        new Vector3(+0.5f, +0.5f, +0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, +0.5f)
                    },
                    new Vector3[] {
                        new Vector3(-0.5f, +0.5f, +0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, -0.5f, +0.5f)
                    },
                    new Vector3[] {
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(-0.5f, +0.5f, -0.5f),
                        new Vector3(+0.5f, +0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f)
                    },
                    new Vector3[] {
                        new Vector3(-0.5f, -0.5f, +0.5f),
                        new Vector3(-0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, -0.5f),
                        new Vector3(+0.5f, -0.5f, +0.5f)
                    }
                };

            public Cube()
                : base(faces)
            { }
        }

        internal class OctaHedron : UniformPolyhedron
        {
            // Mathematica: CForm[PolyhedronData["Octahedron", "Faces", "Polygon"]]
            private static readonly Vector3[][] faces = {
                   new Vector3[] {
                    new Vector3(0, 0, 1.0f / Mathf.Sqrt(2.0f)),
                    new Vector3(0, -(1.0f / Mathf.Sqrt(2.0f)), 0),
                    new Vector3(1.0f / Mathf.Sqrt(2.0f), 0, 0)
                }, new Vector3[] {
                    new Vector3(0,0, 1.0f / Mathf.Sqrt(2.0f)),
                    new Vector3(1.0f / Mathf.Sqrt(2.0f), 0, 0),
                    new Vector3(0, 1.0f / Mathf.Sqrt(2.0f), 0)
                }, new Vector3[] {
                    new Vector3(0, 0, 1.0f / Mathf.Sqrt(2.0f)),
                    new Vector3(0,1.0f / Mathf.Sqrt(2.0f), 0),
                    new Vector3(-(1.0f / Mathf.Sqrt(2.0f)), 0, 0)
                }, new Vector3[] {
                    new Vector3(0, 0, 1.0f / Mathf.Sqrt(2.0f)),
                    new Vector3(-(1.0f / Mathf.Sqrt(2.0f)), 0, 0),
                    new Vector3(0, -(1.0f / Mathf.Sqrt(2.0f)), 0)
                }, new Vector3[] {
                    new Vector3(0, -(1.0f / Mathf.Sqrt(2.0f)), 0),
                    new Vector3(-(1.0f / Mathf.Sqrt(2.0f)), 0, 0),
                    new Vector3(0, 0, -(1.0f / Mathf.Sqrt(2.0f)))
                }, new Vector3[] {
                    new Vector3(0, -(1.0f / Mathf.Sqrt(2.0f)), 0),
                    new Vector3(0, 0, -(1.0f / Mathf.Sqrt(2.0f))),
                    new Vector3(1.0f / Mathf.Sqrt(2.0f), 0, 0)
                }, new Vector3[] {
                    new Vector3(0, 0, -(1.0f / Mathf.Sqrt(2.0f))),
                    new Vector3(-(1.0f / Mathf.Sqrt(2.0f)),0,0),
                    new Vector3(0,1.0f / Mathf.Sqrt(2.0f),0)
                }, new Vector3[] {
                    new Vector3(1.0f / Mathf.Sqrt(2.0f),0,0),
                    new Vector3(0,0,-(1.0f / Mathf.Sqrt(2.0f))),
                    new Vector3(0,1.0f / Mathf.Sqrt(2.0f),0),
                },
            };

            internal OctaHedron()
                : base(faces)
            { }
        }

        internal class Dodecahedron : UniformPolyhedron
        {

            // Mathematica: CForm[PolyhedronData["Dodecahedron", "Faces", "Polygon"]] // N
            //List\(List\((-?\d+\.\d*),\s*(-?\d+\.\d*),\s*(-?\d+\.\d*)\)\)
            private static readonly Vector3[][] faces = {
                new Vector3[] {
                    new Vector3(-1.1135163644116066f, 0.8090169943749475f, -0.2628655560595668f),
                    new Vector3(-0.6881909602355868f, 0.5f, -1.1135163644116066f),
                    new Vector3(-0.6881909602355868f, -0.5f, -1.1135163644116066f),
                    new Vector3(-1.1135163644116066f, -0.8090169943749475f, -0.2628655560595668f),
                    new Vector3(-1.3763819204711736f, 0.0f, 0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(1.3763819204711736f, 0.0f, -0.2628655560595668f),
                    new Vector3(1.1135163644116066f, 0.8090169943749475f, 0.2628655560595668f),
                    new Vector3(0.6881909602355868f, 0.5f, 1.1135163644116066f),
                    new Vector3(0.6881909602355868f, -0.5f, 1.1135163644116066f),
                    new Vector3(1.1135163644116066f, -0.8090169943749475f, 0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(1.1135163644116066f, -0.8090169943749475f, 0.2628655560595668f),
                    new Vector3(0.6881909602355868f, -0.5f, 1.1135163644116066f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, 1.1135163644116066f),
                    new Vector3(-0.42532540417602f, -1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(0.42532540417601994f, -1.3090169943749475f, -0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, -0.5f, 1.1135163644116066f),
                    new Vector3(0.6881909602355868f, 0.5f, 1.1135163644116066f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, 1.1135163644116066f),
                    new Vector3(-0.85065080835204f, 0.0f, 1.1135163644116066f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, 1.1135163644116066f),
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, 0.5f, 1.1135163644116066f),
                    new Vector3(1.1135163644116066f, 0.8090169943749475f, 0.2628655560595668f),
                    new Vector3(0.42532540417601994f, 1.3090169943749475f, -0.2628655560595668f),
                    new Vector3(-0.42532540417602f, 1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, 1.1135163644116066f),
                }, new Vector3[] {
                    new Vector3(1.1135163644116066f, 0.8090169943749475f, 0.2628655560595668f),
                    new Vector3(1.3763819204711736f, 0.0f, -0.2628655560595668f),
                    new Vector3(0.85065080835204f, 0.0f, -1.1135163644116066f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(0.42532540417601994f, 1.3090169943749475f, -0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(1.3763819204711736f, 0.0f, -0.2628655560595668f),
                    new Vector3(1.1135163644116066f, -0.8090169943749475f, 0.2628655560595668f),
                    new Vector3(0.42532540417601994f, -1.3090169943749475f, -0.2628655560595668f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(0.85065080835204f, 0.0f, -1.1135163644116066f),
                }, new Vector3[] {
                    new Vector3(-0.42532540417602f, 1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(0.42532540417601994f, 1.3090169943749475f, -0.2628655560595668f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(-0.6881909602355868f, 0.5f, -1.1135163644116066f),
                    new Vector3(-1.1135163644116066f, 0.8090169943749475f, -0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(0.85065080835204f, 0.0f, -1.1135163644116066f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(-0.6881909602355868f, -0.5f, -1.1135163644116066f),
                    new Vector3(-0.6881909602355868f, 0.5f, -1.1135163644116066f),
                }, new Vector3[] {
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, -1.1135163644116066f),
                    new Vector3(0.42532540417601994f, -1.3090169943749475f, -0.2628655560595668f),
                    new Vector3(-0.42532540417602f, -1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(-1.1135163644116066f, -0.8090169943749475f, -0.2628655560595668f),
                    new Vector3(-0.6881909602355868f, -0.5f, -1.1135163644116066f),
                }, new Vector3[] {
                    new Vector3(-0.42532540417602f, -1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, 1.1135163644116066f),
                    new Vector3(-0.85065080835204f, 0.0f, 1.1135163644116066f),
                    new Vector3(-1.3763819204711736f, 0.0f, 0.2628655560595668f),
                    new Vector3(-1.1135163644116066f, -0.8090169943749475f, -0.2628655560595668f),
                }, new Vector3[] {
                    new Vector3(-0.85065080835204f, 0.0f, 1.1135163644116066f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, 1.1135163644116066f),
                    new Vector3(-0.42532540417602f, 1.3090169943749475f, 0.2628655560595668f),
                    new Vector3(-1.1135163644116066f, 0.8090169943749475f, -0.2628655560595668f),
                    new Vector3(-1.3763819204711736f, 0.0f, 0.2628655560595668f)
                }
            };

            internal Dodecahedron()
                : base(faces)
            { }
        }

        internal class IsocaHedron : UniformPolyhedron
        {
            // Mathematica: CForm[PolyhedronData["Octahedron", "Faces", "Polygon"]] // N
            private static readonly Vector3[][] faces = {
                   new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.9510565162951536f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, 0.5f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.9510565162951536f),
                    new Vector3(-0.6881909602355868f, 0.5f, 0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, -0.5f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.9510565162951536f),
                    new Vector3(-0.6881909602355868f, -0.5f, 0.42532540417601994f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.9510565162951536f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(0.85065080835204f, 0.0f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.0f, 0.0f, 0.9510565162951536f),
                    new Vector3(0.85065080835204f, 0.0f, 0.42532540417601994f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, -0.5f, -0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(0.0f, 0.0f, -0.9510565162951536f)
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, 0.5f, -0.42532540417601994f),
                    new Vector3(0.6881909602355868f, -0.5f, -0.42532540417601994f),
                    new Vector3(0.0f, 0.0f, -0.9510565162951536f)
                }, new Vector3[] {
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(0.6881909602355868f, 0.5f, -0.42532540417601994f),
                    new Vector3(0.0f, 0.0f, -0.9510565162951536f)
                }, new Vector3[] {
                    new Vector3(-0.85065080835204f, 0.0f, -0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(0.0f, 0.0f, -0.9510565162951536f)
                }, new Vector3[] {
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(-0.85065080835204f, 0.0f, -0.42532540417601994f),
                    new Vector3(0.0f, 0.0f, -0.9510565162951536f),
                }, new Vector3[] {
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, 0.5f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(-0.6881909602355868f, 0.5f, 0.42532540417601994f),
                    new Vector3(-0.85065080835204f, 0.0f, -0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, -0.5f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(-0.6881909602355868f, -0.5f, 0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(0.6881909602355868f, -0.5f, -0.42532540417601994f),
                    new Vector3(0.85065080835204f, 0.0f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.85065080835204f, 0.0f, 0.42532540417601994f),
                    new Vector3(0.6881909602355868f, 0.5f, -0.42532540417601994f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, 0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, -0.5f, -0.42532540417601994f),
                    new Vector3(0.2628655560595668f, -0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, -0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(0.6881909602355868f, 0.5f, -0.42532540417601994f),
                    new Vector3(0.85065080835204f, 0.0f, 0.42532540417601994f),
                    new Vector3(0.6881909602355868f, -0.5f, -0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(0.2628655560595668f, 0.8090169943749475f, 0.42532540417601994f),
                    new Vector3(0.6881909602355868f, 0.5f, -0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(-0.85065080835204f, 0.0f, -0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, 0.5f, 0.42532540417601994f),
                    new Vector3(-0.2628655560595668f, 0.8090169943749475f, -0.42532540417601994f)
                }, new Vector3[] {
                    new Vector3(-0.2628655560595668f, -0.8090169943749475f, -0.42532540417601994f),
                    new Vector3(-0.6881909602355868f, -0.5f, 0.42532540417601994f),
                    new Vector3(-0.85065080835204f, 0.0f, -0.42532540417601994f)
                }
            };

            internal IsocaHedron()
                : base(faces)
            { }
        }
    }
}