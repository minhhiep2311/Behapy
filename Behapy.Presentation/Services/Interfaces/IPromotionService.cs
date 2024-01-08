namespace Behapy.Presentation.Services.Interfaces;

public interface IPromotionService
{
    List<string> GetAllContent(bool getHidden = false);
}