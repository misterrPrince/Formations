using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FivePointStarFormation : FormationBase
{
    [SerializeField] private int  _density = 1;
    public override  IEnumerable<Vector3> EvaluatePoints() {
        List<Vector3> fivePoints = new List<Vector3>();
        for(var i = 0 ; i < 5 ; i++)
        {
            var x = Mathf.Cos(i * 72f  * Mathf.Deg2Rad) * _spread;
            var z = Mathf.Sin(i * 72f * Mathf.Deg2Rad) * _spread ;
            var pos = new Vector3( x , 0 , z);
            fivePoints.Add(pos);
        }
        var final = new List<List<Vector3>>();
       final.Add(fivePoints);
       final.Add(GetDirecitonalPoints(fivePoints[0] , fivePoints[1]));
       final.Add(GetDirecitonalPoints(fivePoints[1] , fivePoints[2]));
       final.Add(GetDirecitonalPoints(fivePoints[2] , fivePoints[3]));
       final.Add(GetDirecitonalPoints(fivePoints[3] , fivePoints[4]));
       final.Add(GetDirecitonalPoints(fivePoints[4] , fivePoints[0]));
       foreach(var l in  final)
       {
           foreach(Vector3 pos in l)
            {
                yield return pos;
            }
       }
    }

    private List<Vector3> GetDirecitonalPoints(Vector3 startPoint , Vector3 endPoint)
    {  
         /* we know the length of   inner pentagon edge  so we can  use Sinus teorem to calculate the distance betwwen inner pentagon points ad star corner points
        
        angle : refers the angle between star edges
        in 5 point star edge angle is 36 
        */

        var angle = 90f -72f; 
        angle *=2;
        var propotion = Mathf.Sin(Mathf.Deg2Rad *72) / Mathf.Sin(Mathf.Deg2Rad * angle) ;
        var tempDensity = propotion * 1;

        var dir = startPoint - endPoint;
        var AllPoints = new List<Vector3>();
    
        for(int j = 1; j< _density  + 1 ; j++)
        {
            AllPoints.Add(startPoint + dir * _spread * (float ) j  * (float) tempDensity /(_density * _spread) );
            if(j != _density) // To Avoid duplicated corner points we need to remove one of 2 corner points for each corner
                AllPoints.Add(endPoint - dir * _spread * (float ) j  * (float) tempDensity /(_density * _spread) ); // Getting symmetry here
        }
        return AllPoints;
    }
}
