public enum UIMode { Default, Game, Shop }

public interface IUIActive
{
    public void SetMode(UIMode mode);
}
