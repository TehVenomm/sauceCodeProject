.class public Ljp/colopl/libs/ColoplCellLocationListener;
.super Landroid/telephony/PhoneStateListener;
.source "ColoplCellLocationListener.java"


# static fields
.field public static final ERROR_TYPE_FAIL_REFLECTION:I = 0x1

.field public static final ERROR_TYPE_NO_ERROR:I = 0x0

.field public static final ERROR_TYPE_NULL_LOCATION:I = 0x2


# instance fields
.field private callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

.field private errorType:I


# direct methods
.method public constructor <init>(Ljp/colopl/libs/ColoplCellLocationListenerCallback;)V
    .locals 1

    .line 30
    invoke-direct {p0}, Landroid/telephony/PhoneStateListener;-><init>()V

    const/4 v0, 0x0

    .line 16
    iput-object v0, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    const/4 v0, 0x0

    .line 17
    iput v0, p0, Ljp/colopl/libs/ColoplCellLocationListener;->errorType:I

    .line 31
    iput-object p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    return-void
.end method

.method public static startListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V
    .locals 1

    const-string v0, "phone"

    .line 20
    invoke-virtual {p0, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/telephony/TelephonyManager;

    const/16 v0, 0x10

    .line 21
    invoke-virtual {p0, p1, v0}, Landroid/telephony/TelephonyManager;->listen(Landroid/telephony/PhoneStateListener;I)V

    return-void
.end method

.method public static stopListenCellLocationChange(Landroid/content/Context;Ljp/colopl/libs/ColoplCellLocationListener;)V
    .locals 1

    const-string v0, "phone"

    .line 25
    invoke-virtual {p0, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/telephony/TelephonyManager;

    const/4 v0, 0x0

    .line 26
    invoke-virtual {p0, p1, v0}, Landroid/telephony/TelephonyManager;->listen(Landroid/telephony/PhoneStateListener;I)V

    return-void
.end method


# virtual methods
.method public getErrorType()I
    .locals 1

    .line 58
    iget v0, p0, Ljp/colopl/libs/ColoplCellLocationListener;->errorType:I

    return v0
.end method

.method public onCellLocationChanged(Landroid/telephony/CellLocation;)V
    .locals 1

    .line 36
    invoke-static {p1}, Ljp/colopl/libs/CdmaCellLocationRef;->getCastedInstance(Landroid/telephony/CellLocation;)Ljp/colopl/libs/CdmaCellLocationRef;

    move-result-object p1

    if-nez p1, :cond_1

    .line 39
    iget-object p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    if-eqz p1, :cond_0

    const/4 p1, 0x1

    .line 40
    iput p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->errorType:I

    .line 41
    iget-object p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    invoke-interface {p1, p0}, Ljp/colopl/libs/ColoplCellLocationListenerCallback;->receiveFailedCdmaCellLocation(Ljp/colopl/libs/ColoplCellLocationListener;)V

    :cond_0
    return-void

    .line 45
    :cond_1
    invoke-virtual {p1}, Ljp/colopl/libs/CdmaCellLocationRef;->getLocation()Landroid/location/Location;

    move-result-object p1

    .line 46
    iget-object v0, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    if-eqz v0, :cond_3

    if-nez p1, :cond_2

    const/4 p1, 0x2

    .line 48
    iput p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->errorType:I

    .line 49
    iget-object p1, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    invoke-interface {p1, p0}, Ljp/colopl/libs/ColoplCellLocationListenerCallback;->receiveFailedCdmaCellLocation(Ljp/colopl/libs/ColoplCellLocationListener;)V

    goto :goto_0

    .line 52
    :cond_2
    iget-object v0, p0, Ljp/colopl/libs/ColoplCellLocationListener;->callback:Ljp/colopl/libs/ColoplCellLocationListenerCallback;

    invoke-interface {v0, p0, p1}, Ljp/colopl/libs/ColoplCellLocationListenerCallback;->receiveSuccessCdmaCellLocation(Ljp/colopl/libs/ColoplCellLocationListener;Landroid/location/Location;)V

    :cond_3
    :goto_0
    return-void
.end method
