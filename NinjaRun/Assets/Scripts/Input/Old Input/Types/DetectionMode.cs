namespace Input.Old_Input.Types
{
    public enum DetectionMode
    {
        /// <summary>
        /// Completed swipe detection mode. Wait finger release.
        /// </summary>
        Completed = 0,
            
        /// <summary>
        /// Uncompleted swipe detection mode. No wait finger release.
        /// </summary>
        Uncompleted = 1,
        
        Swipe = 2
    }

    public enum ActionMaps
    {
        Touch = 0,
        UI = 1
        
    }
}