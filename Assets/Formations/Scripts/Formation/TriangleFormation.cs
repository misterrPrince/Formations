using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleFormation :  FormationBase {
    [SerializeField] private int _quantity;
    [SerializeField] private float _unitDistanceHorizontal = 1;
    [SerializeField] private float _unitDistanceVertical = 1;
    

    public override  IEnumerable<Vector3> EvaluatePoints() {
        
        var c = _quantity;

        
        for (var i = 0; i < _quantity; i++) {
                for(var j = 0; j < c ; j ++)
                    {
                        var x = (float)(i )/2 * _unitDistanceHorizontal + j * _unitDistanceHorizontal;
                        var z = i * _unitDistanceVertical;

                        var pos = new Vector3(x, 0, z);

                        pos += GetNoise(pos);

                        pos *= _spread;

                        yield return pos;
                    }
                c -=1;     
            }

           
        }
    
}