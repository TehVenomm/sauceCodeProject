.class Lnet/gogame/gowrap/ui/MainActivity$5;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/MainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/MainActivity;)V
    .locals 0

    .line 87
    iput-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$5;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 91
    iget-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$5;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/MainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object p1

    instance-of p1, p1, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    if-nez p1, :cond_0

    .line 92
    new-instance p1, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    invoke-direct {p1}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;-><init>()V

    .line 93
    iget-object v0, p0, Lnet/gogame/gowrap/ui/MainActivity$5;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/ui/MainActivity;->pushFragment(Landroid/app/Fragment;)V

    :cond_0
    return-void
.end method
