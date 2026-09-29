.class Lcom/helpshift/support/HSApiData$2;
.super Ljava/lang/Object;
.source "HSApiData.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/HSApiData;->startSearchIndexing()V
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

    .line 186
    iput-object p1, p0, Lcom/helpshift/support/HSApiData$2;->this$0:Lcom/helpshift/support/HSApiData;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 188
    iget-object v0, p0, Lcom/helpshift/support/HSApiData$2;->this$0:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v0}, Lcom/helpshift/support/HSApiData;->updateIndex()V

    return-void
.end method
