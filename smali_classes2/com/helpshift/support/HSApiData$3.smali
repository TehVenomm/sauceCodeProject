.class Lcom/helpshift/support/HSApiData$3;
.super Ljava/lang/Object;
.source "HSApiData.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/HSApiData;->loadIndex()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/HSApiData;


# direct methods
.method constructor <init>(Lcom/helpshift/support/HSApiData;)V
    .locals 0

    .line 464
    iput-object p1, p0, Lcom/helpshift/support/HSApiData$3;->this$0:Lcom/helpshift/support/HSApiData;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 467
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/support/HSApiData$3;->this$0:Lcom/helpshift/support/HSApiData;

    iget-object v0, v0, Lcom/helpshift/support/HSApiData;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {v0}, Lcom/helpshift/support/HSStorage;->loadIndex()V
    :try_end_0
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "Helpshift_ApiData"

    const-string v2, "Exception while loading index: trying to re-create the index"

    .line 470
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 475
    iget-object v0, p0, Lcom/helpshift/support/HSApiData$3;->this$0:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v0}, Lcom/helpshift/support/HSApiData;->updateIndex()V

    .line 477
    :try_start_1
    iget-object v0, p0, Lcom/helpshift/support/HSApiData$3;->this$0:Lcom/helpshift/support/HSApiData;

    iget-object v0, v0, Lcom/helpshift/support/HSApiData;->storage:Lcom/helpshift/support/HSStorage;

    invoke-virtual {v0}, Lcom/helpshift/support/HSStorage;->loadIndex()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_0

    :catch_1
    move-exception v0

    const-string v1, "Helpshift_ApiData"

    const-string v2, "Exception caught again, while loading index: "

    .line 480
    invoke-static {v1, v2, v0}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method
