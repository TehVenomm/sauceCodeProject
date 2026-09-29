.class Lnet/gree/unitywebview/WebViewPluginInterface;
.super Ljava/lang/Object;
.source "WebViewPluginInterface.java"


# instance fields
.field private mGameObject:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPluginInterface;->mGameObject:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public call(Ljava/lang/String;)V
    .locals 2
    .annotation runtime Landroid/webkit/JavascriptInterface;
    .end annotation

    .line 18
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPluginInterface;->mGameObject:Ljava/lang/String;

    const-string v1, "CallFromJS"

    invoke-static {v0, v1, p1}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method
