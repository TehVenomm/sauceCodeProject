.class Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/dpro/view/CustomTabbelPanel$Listener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private currentTabIndex:I

.field final synthetic this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V
    .locals 0

    .line 34
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 36
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->currentTabIndex:I

    return-void
.end method


# virtual methods
.method public onClose()V
    .locals 1

    .line 40
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->finish()V

    return-void
.end method

.method public onTabSelected(I)V
    .locals 2

    .line 45
    iget v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->currentTabIndex:I

    if-ne p1, v0, :cond_0

    return-void

    :cond_0
    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 56
    :pswitch_0
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->clearFragments()V

    .line 57
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-direct {v1}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;-><init>()V

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->pushFragment(Landroid/app/Fragment;)V

    goto :goto_0

    .line 51
    :pswitch_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->clearFragments()V

    .line 52
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->access$000(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception v0

    .line 64
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->currentTabIndex:I

    .line 65
    throw v0

    .line 64
    :goto_0
    iput p1, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$1;->currentTabIndex:I

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
