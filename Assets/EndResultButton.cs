using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UnityEngine.Localization
{
    public class EndResultButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool mouseOver;
        public bool isResetButton;
        Move playerscript;
        UIUpdater ui;

        float scaleUp;
        public float scaleMultiplier;
        // Update is called once per frame
        private void Start()
        {
            playerscript = GameObject.Find("Player").GetComponent<Move>();
            ui = GameObject.Find("Canvas").GetComponent<UIUpdater>();
        }
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && mouseOver)
            {
                if (isResetButton) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
                else 
                {
                    StartFreePlay();
                }
            }
            if (mouseOver)
            {
                if (scaleUp < 0.2f)
                {
                    transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 1) * (1 + scaleUp), 1.0f);
                    scaleUp += 1f * Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    if (isResetButton == true)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    if (isResetButton == false) 
                    {
                        StartFreePlay();
                    }
                    
                }
            }
            if (!mouseOver)
            {
                if (scaleUp > 0f)
                {
                    transform.localScale = new Vector3(scaleMultiplier * (1 + scaleUp), (scaleMultiplier * 1) * (1 + scaleUp), 1.0f);
                    scaleUp -= 1f * Time.deltaTime;
                }
            }
        }
        void StartFreePlay()
        {
            playerscript.freeplay = true;
            playerscript.disableInput = false;
            ui.transform.Find("EndMessage").gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseOver = true;
            Debug.Log("Mouse enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOver = false;
            Debug.Log("Mouse exit");
        }
    }
}
