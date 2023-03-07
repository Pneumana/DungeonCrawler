using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.Localization
{
    public class FadeIn : MonoBehaviour
    {
        public bool fadeMe;
        float alpha = 1;
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        }

        // Update is called once per frame
        void Update()
        {
            if (fadeMe && alpha > 0)
            {
                alpha -= 1.0f * Time.deltaTime;
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Max(alpha, 0));
            }
            if(alpha <= 0) { this.gameObject.SetActive(false); }
        }
    }
}
