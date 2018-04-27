namespace Entity.Service
{
    public enum CallStatusType
    {
        None = 0,
        DocketRequestInQueue = 1,
        DocketOpenForSpares = 2,
        DocketOpenForConsumables = 3,
        DocketOpenForSeniorEngineer =4,
        DocketOpenForApproval = 5,
        DocketFunctional = 6,
        DocketResponseGiven = 12,
        DocketClose = 11,
        TonerRequestInQueue = 7,
        TonerOpenForApproval =8,
        TonerDelivered = 9,
        TonerRejected = 10,
        TonerResponseGiven = 13,
    }
}
