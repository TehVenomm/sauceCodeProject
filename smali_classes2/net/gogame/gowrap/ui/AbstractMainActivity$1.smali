.class Lnet/gogame/gowrap/ui/AbstractMainActivity$1;
.super Ljava/lang/Object;
.source "AbstractMainActivity.java"

# interfaces
.implements Lnet/gogame/gowrap/GoWrapImpl$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/AbstractMainActivity;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V
    .locals 0

    .line 45
    iput-object p1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;->this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onOffersAvailable()V
    .locals 1

    .line 58
    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;->this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->enableOffers()V

    return-void
.end method

.method public onVipStatusUpdated(Lnet/gogame/gowrap/VipStatus;)V
    .locals 1

    .line 49
    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;->this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;

    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->access$000(Lnet/gogame/gowrap/ui/AbstractMainActivity;Lnet/gogame/gowrap/VipStatus;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 50
    iget-object p1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;->this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->access$100(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V

    goto :goto_0

    .line 52
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;->this$0:Lnet/gogame/gowrap/ui/AbstractMainActivity;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->access$200(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V

    :goto_0
    return-void
.end method
