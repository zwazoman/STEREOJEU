public class ButtonInteraction : Interaction
{
    public bool WasPress { get; private set; }

    public override void InteractionStart() 
    {
        if (!IsActive) return;
        WasPress = true;
    }

    public override void InteractionStop() { }

    public void ResetState() { WasPress = false; }
}
