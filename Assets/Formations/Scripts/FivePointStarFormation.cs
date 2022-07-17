using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FivePointStarFormation : FormationBase
{
    [SerializeField] int  _density = 1;


    public override  IEnumerable<Vector3> EvaluatePoints() {
        List<Vector3> fivePoints = new List<Vector3>();
        for(int i = 0 ; i < 5 ; i++)
        {
            var x = Mathf.Cos(i * 72f  * Mathf.Deg2Rad) * Spread;
            var z = Mathf.Sin(i * 72f * Mathf.Deg2Rad) * Spread ;

            var pos = new Vector3( x , 0 , z);
            fivePoints.Add(pos);
             

        }
        

       List< List<Vector3>> final = new List<List<Vector3>>();
       final.Add(fivePoints);
       final.Add(GetDirecitonalPoints(fivePoints[0] , fivePoints[1]));
       final.Add(GetDirecitonalPoints(fivePoints[1] , fivePoints[2]));
       final.Add(GetDirecitonalPoints(fivePoints[2] , fivePoints[3]));
       final.Add(GetDirecitonalPoints(fivePoints[3] , fivePoints[4]));
       final.Add(GetDirecitonalPoints(fivePoints[4] , fivePoints[0]));

       foreach(List<Vector3> l in  final)
       {
           foreach(Vector3 pos in l)
            {
                yield return pos;
            }
       }
       
       
    }

    List<Vector3> GetDirecitonalPoints(Vector3 startPoint , Vector3 endPoint)
    {  
         /* we know the length of   inner pentagon edge  so we can  use Sinus teorem to calculate the distance betwwen inner pentagon points ad star corner points
        
        angle : refers the angle between star edges
        in 5 point star edge angle is 36 
        */

        float angle = 90f -72f; 
        angle *=2;
        float propotion = Mathf.Sin(Mathf.Deg2Rad *72) / Mathf.Sin(Mathf.Deg2Rad * angle) ;
        float tempDensity = propotion * 1;

        Vector3 dir = startPoint - endPoint;
        List<Vector3> AllPoints = new List<Vector3>();
    
        for(int j = 1; j< _density  + 1 ; j++)
        {
            AllPoints.Add(startPoint + dir * Spread * (float ) j  * (float) tempDensity /(_density * Spread) );
            
            
            if(j != _density) // To Avoid duplicated corner points we need to remove one of 2 corner points for each corner
                AllPoints.Add(endPoint - dir * Spread * (float ) j  * (float) tempDensity /(_density * Spread) ); // Getting symmetry here

        }

        return AllPoints;
    }
}
