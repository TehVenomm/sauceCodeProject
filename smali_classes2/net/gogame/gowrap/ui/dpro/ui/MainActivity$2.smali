.class Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 74
    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    .line 75
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 77
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 78
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dpro/ui/MainActivity$2;->this$0:Lnet/gogame/gowrap/ui/dpro/ui/MainActivity;

    .line 79
    invoke-virtual {p1}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object p1

    .line 78
    invoke-static {v0, p1}, Lnet/gogame/gowrap/ui/utils/ExternalAppLauncher;->openUrlInExternalBrowser(Landroid/app/Activity;Ljava/lang/String;)Z

    :cond_0
    return-void
.end method
