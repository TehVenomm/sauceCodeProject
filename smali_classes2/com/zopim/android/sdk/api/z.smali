.class Lcom/zopim/android/sdk/api/z;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/x;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/x;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/z;->a:Lcom/zopim/android/sdk/api/x;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/z;->a:Lcom/zopim/android/sdk/api/x;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/x;->a(Lcom/zopim/android/sdk/api/x;Landroid/webkit/WebView;)Landroid/webkit/WebView;

    return-void
.end method
