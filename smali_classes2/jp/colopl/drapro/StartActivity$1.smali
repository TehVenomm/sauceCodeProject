.class Ljp/colopl/drapro/StartActivity$1;
.super Ljava/lang/Object;
.source "StartActivity.java"

# interfaces
.implements Ljp/colopl/iab/IabBroadcastReceiver$IabBroadcastListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/StartActivity;->initPromoCodeReceiver()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/StartActivity;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/StartActivity;)V
    .locals 0

    .line 224
    iput-object p1, p0, Ljp/colopl/drapro/StartActivity$1;->this$0:Ljp/colopl/drapro/StartActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public receivedBroadcast()V
    .locals 3

    const-string v0, "PromoCode"

    const-string v1, "Receive API Code !!!!!!!!!!"

    .line 227
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "ShopReceiver"

    const-string v1, "promoteCheck"

    const-string v2, ""

    .line 228
    invoke-static {v0, v1, v2}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
