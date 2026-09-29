.class Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;
.super Ljava/lang/Object;
.source "CommunityFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->setup(Landroid/view/View;ILjava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

.field final synthetic val$url:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;Ljava/lang/String;)V
    .locals 0

    .line 58
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;->val$url:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 62
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;->this$0:Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;->getActivity()Landroid/app/Activity;

    move-result-object p1

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment$1;->val$url:Ljava/lang/String;

    invoke-static {p1, v0}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z

    return-void
.end method
