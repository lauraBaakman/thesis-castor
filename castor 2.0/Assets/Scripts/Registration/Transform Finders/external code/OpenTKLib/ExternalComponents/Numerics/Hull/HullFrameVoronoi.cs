﻿using System.Collections.Generic;
using OpenTKLib;


namespace NLinear
{
    public class HullFrame
    {
        public HullFrame() { }
        public static Box Box;
        public static Edge Edge;
        
        //public List<Line3<double>> Offset3D(List<Polyline<double>> x, double y)
        //{
        //    List<Line3<double>> output = new List<Line3<double>>();
        //    if (x.Count < 4) return output;
        //    List<Line3<double>> lines = breakPoly(x[0]);

        //    for (int i = 1; i < x.Count; i++)
        //    {
        //        List<Line3<double>> ls = breakPoly(x[i]);
        //        //Print(ls.Count.ToString());
        //        for (int ii = 0; ii < ls.Count; ii++)
        //        {
        //            bool sign = true;
        //            for (int j = 0; j < lines.Count; j++)
        //            {
        //                if (isDumpLines(lines[j], ls[ii])) { sign = false; break; }
        //            }
        //            //Print(sign.ToString());
        //            if (sign) lines.Add(ls[ii]);
        //        }
        //    }
        //    Vector3<double> cen = new Vector3<double>();
        //    for (int i = 0; i < lines.Count; i++)
        //    {
        //        cen += lines[i].From; cen += lines[i].To;
        //    }
        //    // B = lines;
        //    cen /= 2 * lines.Count;
        //    BoxVoronoi box = new BoxVoronoi(lines);
        //    HullVoronoi hull = new HullVoronoi(box, cen);
        //    for (int i = 0; i < x.Count; i++)
        //    {
        //        if (x[i].Count < 3)
        //        {//Print("00001");
        //            return output;
        //        }
        //        Polyline<double> cp = x[i];
        //        Vector3<double> vi0 = cp[0];

        //        Vector3<double> xi0 = x[i][0];


        //        Plane3<double> p = new Plane3<double>(x[i][0], x[i][1], x[i][2], 1);
                
        //        Vector3<double> v = cen - p.ClosestPoint(cen);
        //        v.Normalize(1);
                
        //        p = new Plane3<double>(x[i][0], v, 1);
        //        p.Transform(Transform.Translation(v * y));
        //        hull.intersect(p);
        //        hull.clearnull();
        //    }

        //    for (int i = 0; i < hull.edges.Count; i++)
        //    {
        //        output.Add(new Line3<double>(hull.edges[i].p1.pos, hull.edges[i].p2.pos));
        //    }
        //    List<Vector3<double>> pt = new List<Vector3<double>>();
        //    for (int i = 0; i < hull.pts.Count; i++)
        //    {
        //        pt.Add(hull.pts[i].pos);
        //    }
        //    return output;
        //}
        public List<Line3<double>> BreakPoly(Polyline<double> pl)
        {
            List<Line3<double>> ls = new List<Line3<double>>();
            
            Vector3<double> v = pl[0];

            if (pl.Count < 1) return ls;
            for (int i = 1; i < pl.Count; i++)
            {
                ls.Add(new Line3<double>(pl[i], pl[i - 1], 1));
            }
            return ls;
        }
        public bool IsDumpLines(Line3<double> l1, Line3<double> l2)
        {
            //if ((l1.From.DistanceTo(l2.From) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance) && (l1.To.DistanceTo(l2.To) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)) return true;
            //if ((l1.From.DistanceTo(l2.To) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance) && (l1.To.DistanceTo(l2.From) < RhinoDoc.ActiveDoc.ModelAbsoluteTolerance)) return true;

            if ((l1.From.DistanceTo(l2.From, 1) < GeneralSettings.AbsoluteTolerance) && (l1.To.DistanceTo(l2.To, 1) < GeneralSettings.AbsoluteTolerance)) 
                return true;
            if ((l1.From.DistanceTo(l2.To, 1) < GeneralSettings.AbsoluteTolerance) && (l1.To.DistanceTo(l2.From, 1) < GeneralSettings.AbsoluteTolerance)) 
                return true;

            return false;
        }
    }
}
