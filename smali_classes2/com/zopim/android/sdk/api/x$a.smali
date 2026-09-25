.class Lcom/zopim/android/sdk/api/x$a;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/x;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "a"
.end annotation


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/x;

.field private final b:Landroid/webkit/WebView;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/x;Landroid/webkit/WebView;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/x$a;->a:Lcom/zopim/android/sdk/api/x;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iput-object p2, p0, Lcom/zopim/android/sdk/api/x$a;->b:Landroid/webkit/WebView;

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x$a;->b:Landroid/webkit/WebView;

    if-nez v0, :cond_0

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/x$a;->b:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->stopLoading()V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/x$a;->b:Landroid/webkit/WebView;

    invoke-virtual {v0}, Landroid/webkit/WebView;->destroy()V

    return-void
.end method
