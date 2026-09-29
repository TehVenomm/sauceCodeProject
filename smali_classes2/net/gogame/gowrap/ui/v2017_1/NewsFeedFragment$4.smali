.class Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;
.super Ljava/lang/Object;
.source "NewsFeedFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;)V
    .locals 0

    .line 178
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 182
    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    .line 183
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 185
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 186
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment$4;->this$0:Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    .line 187
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object p1

    .line 186
    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z

    :cond_0
    return-void
.end method
