using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGerador : MonoBehaviour
{
    [SerializeField] private int maxSlider;
    public List<Transform> _sliders = new List<Transform>();
    public static float slider1;

    private void Awake()
    {
        

        maxSlider = 10;
        //_slider.onValueChanged.AddListener(SlideChange);
        GetAllChildren(transform);
        foreach (Transform child in _sliders)
        {
           
            // Tenta obter o componente Slider do objeto filho
            Slider sliderComponent = child.GetComponent<Slider>();

            // Verifica se o componente Slider foi encontrado
            if (sliderComponent != null)
            {
                // Faça algo com o componente Slider, por exemplo, imprima seu valor atual
                Debug.Log("Slider do objeto filho (" + child.name + ")" );
            }
        }

       }
    private void GetAllChildren(Transform parent)
    {
        // Adiciona o pai à lista
        _sliders.Add(parent);

        // Itera sobre os filhos do pai
        foreach (Transform child in parent)
        {
            // Chama recursivamente para cada objeto filho
            GetAllChildren(child);
        }
    }
        void SlideChange(float valor)
    {
        float localValue = Mathf.RoundToInt( valor * maxSlider);
        slider1 = localValue;
        Debug.Log(localValue);
   
    }

}
