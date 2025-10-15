public class Press : Interactable
{
    public bool WasPress { get; private set; }

    public override void InteractionStart() { WasPress = true; }

    public override void InteractionStop()
    {
        throw new System.NotImplementedException();
    }

    public void ResetState() { WasPress = false; }
}
