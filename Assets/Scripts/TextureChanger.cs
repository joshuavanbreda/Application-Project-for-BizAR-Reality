using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Texture[] textureList;
    public GameObject enemyCanvas;

    public ParticleSystem customExplosion;

    public void ChangeTexture(Rigidbody rb)
    {
        if (enemyCanvas.activeSelf)
        {
            enemyCanvas.SetActive(false);
        }

        rb.gameObject.GetComponent<MeshRenderer>().material.mainTexture = textureList[Random.Range(0, textureList.Length)];
        ParticleSystem newExplosion = Instantiate(customExplosion, rb.gameObject.transform.position, Quaternion.identity, null);
        Destroy(newExplosion, 2f);
    }
}
