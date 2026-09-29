.class final Lcom/appsflyer/i$1;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/appsflyer/i;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private synthetic ˊ:Lcom/appsflyer/i;

.field private synthetic ॱ:Ljava/util/Map;


# direct methods
.method constructor <init>(Lcom/appsflyer/i;Ljava/util/Map;)V
    .locals 0

    .line 81
    iput-object p1, p0, Lcom/appsflyer/i$1;->ˊ:Lcom/appsflyer/i;

    iput-object p2, p0, Lcom/appsflyer/i$1;->ॱ:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 4

    .line 84
    iget-object v0, p0, Lcom/appsflyer/i$1;->ˊ:Lcom/appsflyer/i;

    iget-object v1, p0, Lcom/appsflyer/i$1;->ॱ:Ljava/util/Map;

    iget-object v2, p0, Lcom/appsflyer/i$1;->ˊ:Lcom/appsflyer/i;

    invoke-static {v2}, Lcom/appsflyer/i;->ˎ(Lcom/appsflyer/i;)Ljava/util/Map;

    move-result-object v2

    iget-object v3, p0, Lcom/appsflyer/i$1;->ˊ:Lcom/appsflyer/i;

    invoke-static {v3}, Lcom/appsflyer/i;->ˏ(Lcom/appsflyer/i;)Ljava/lang/ref/WeakReference;

    move-result-object v3

    invoke-static {v0, v1, v2, v3}, Lcom/appsflyer/i;->ॱ(Lcom/appsflyer/i;Ljava/util/Map;Ljava/util/Map;Ljava/lang/ref/WeakReference;)V

    return-void
.end method
