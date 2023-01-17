using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isFaceRight = true;
    private float _coordinataX;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_coordinataX = _speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_coordinataX = _speed * Time.deltaTime * -1, 0, 0);
        }

        if(_coordinataX > 0 && !_isFaceRight)
        {
            Flip();
        }
        else if(_coordinataX < 0 && _isFaceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFaceRight = !_isFaceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
