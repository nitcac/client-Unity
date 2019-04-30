using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Security.Cryptography;

public class APIManeger : MonoBehaviour
{
    string text, iv = null, key, postToken, postIV;
    int count;
    private static string numAndWords = "0123456789ABCDEFGHIJKLNMOPQRSTUVWXYZabcdefghijklnmopqrstuvwxyz";
    byte[] tokenEncode, ivEncode, encryptBytes;
    AesManaged aes = new AesManaged();


    // Start is called before the first frame update
    void Start()
    {
        key = "XiZEFzMPsvbIaanO";
        count = 16;
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        StartCoroutine(GetToken());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetToken()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://ik1-309-14793.vs.sakura.ne.jp/api/auth/token");
        yield return request.SendWebRequest();

        if (!request.isNetworkError)
        {
            text = request.downloadHandler.text;
            //Debug.Log(text);
            var json = JsonUtility.FromJson(text, typeof(Token)) as Token;
            //Debug.Log(json.token);
            tokenEncode = Encoding.UTF8.GetBytes(json.token);
            for (int i = 0; i < count; i++)
            {
                iv += numAndWords[Random.Range(0, numAndWords.Length)];
            }
            ivEncode = Encoding.UTF8.GetBytes(iv);
            aes.IV = ivEncode;
            encryptBytes = aes.CreateEncryptor().TransformFinalBlock(tokenEncode, 0, tokenEncode.Length);//AES暗号化
            postToken = System.Convert.ToBase64String(encryptBytes);
            postIV = System.Convert.ToBase64String(aes.IV);
            StartCoroutine(PostToken());
            yield break;
        }
    }

    IEnumerator PostToken()
    {
        UnityWebRequest request = UnityWebRequest.Post("http://ik1-309-14793.vs.sakura.ne.jp/api/auth", "");
        request.SetRequestHeader("encrypted", postToken);
        request.SetRequestHeader("iv", postIV);
        yield return request.SendWebRequest();
        if (!request.isNetworkError)
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

}
