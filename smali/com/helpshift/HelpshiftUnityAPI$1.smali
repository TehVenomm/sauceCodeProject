.class final Lcom/helpshift/HelpshiftUnityAPI$1;
.super Ljava/lang/Object;
.source "HelpshiftUnityAPI.java"

# interfaces
.implements Lcom/helpshift/support/AlertToRateAppListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/HelpshiftUnityAPI;->showAlertToRateApp(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 206
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onAction(I)V
    .locals 2

    const-string v0, ""

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    const-string v0, "HS_RATE_ALERT_FAIL"

    goto :goto_0

    :pswitch_1
    const-string v0, "HS_RATE_ALERT_CLOSE"

    goto :goto_0

    :pswitch_2
    const-string v0, "HS_RATE_ALERT_FEEDBACK"

    goto :goto_0

    :pswitch_3
    const-string v0, "HS_RATE_ALERT_SUCCESS"

    .line 224
    :goto_0
    invoke-static {}, Lcom/helpshift/HelpshiftUnityAPI;->access$000()Ljava/lang/String;

    move-result-object p1

    const-string v1, "alertToRateAppAction"

    .line 225
    invoke-static {p1, v1, v0}, Lcom/helpshift/util/UnityUtils;->sendUnityMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
