using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrescentFormation : FormationBase
{
    [SerializeField] private float _range=5;
    [SerializeField] private int _circleCount=5;
    [SerializeField] private int _density=20;
    [SerializeField] float _unitDistance=0.3f;
    [SerializeField] int _unitDecrease=2;
    
    

    public override  IEnumerable<Vector3> EvaluatePoints() {
        
        var half = 180;
        float potion = (half / _density);
        int tempDensity =  _density;
        float tempRange = _range;
        float offset = 0;

        
        for (var i = 0; i < _circleCount ; i++) {
                for(var j = 0; j < tempDensity ; j ++)
                    {
                        var x = (Mathf.Sin(Mathf.Deg2Rad * (j + offset)* potion ) * tempRange);
                        var z = (Mathf.Cos(Mathf.Deg2Rad * (j + offset) * potion) * tempRange);

                        var pos = new Vector3(x, 0, z);

                        pos += GetNoise(pos);

                        pos *= _spread;

                        yield return pos;
                    }
               
                
                potion /= 1 +(((float)_unitDecrease /20));
                offset += (float)_unitDecrease; 
                tempDensity -= _unitDecrease;
                
                
                tempRange = tempRange -_unitDistance;

                   
            }

           
        }
    
}