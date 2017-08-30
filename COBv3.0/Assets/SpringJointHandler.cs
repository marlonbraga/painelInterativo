using UnityEngine;

public class SpringJointHandler : MonoBehaviour
{
    Vector3 lastPosition = new Vector3(0, 0, 0);
    float lastVelocity;

    void Update()
    {
        var velocity = (transform.parent.position - lastPosition).magnitude / Time.deltaTime;//Faz a velocidade.
        lastPosition = transform.parent.position;//Armazena a última posição

        if (velocity > lastVelocity)//Checa se ele está se movendo.
        {
            //GetComponent<SpringJoint>().spring /= 10;//Tira a tensão do spring.
        }
        else
        {
            //GetComponent<SpringJoint>().spring *= 10;//Coloca tensão.
        }

        lastVelocity = velocity;//Salva a última velocidade.

        //if (GetComponent<SpringJoint>().spring > 9999)//Não deixa ele fica muito tensionado, caso o avatar fique parado por muito tempo.
        //{
        //    GetComponent<SpringJoint>().spring = 9999;
        //}

        //if (GetComponent<SpringJoint>().spring < 1)//Não deixa que fique pouco tensionado.
        //{
        //    GetComponent<SpringJoint>().spring = 1;
        //}

        GetComponent<LineRenderer>().SetPosition(0, transform.parent.position);//Desenha a linha que estamos vendo.
        GetComponent<LineRenderer>().SetPosition(1, transform.position);
    }
}
