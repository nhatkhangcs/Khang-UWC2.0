using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class LoadPanel : MonoBehaviour, IHideAnimatable
{
    [SerializeField] private Image image;

    public Task AnimateHide()
    {
        return image.DOFade(0f, 1f)
            .OnComplete(() => gameObject.SetActive(false))
            .AsyncWaitForCompletion();
    }
}