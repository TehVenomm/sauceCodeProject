.class public Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;
.super Ljava/lang/Object;
.source "DoubleMetaphone.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/external/DoubleMetaphone;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "DoubleMetaphoneResult"
.end annotation


# instance fields
.field final alternate:Ljava/lang/StringBuilder;

.field final maxLength:I

.field final primary:Ljava/lang/StringBuilder;

.field final synthetic this$0:Lcom/helpshift/support/external/DoubleMetaphone;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/external/DoubleMetaphone;I)V
    .locals 1

    .line 1327
    iput-object p1, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->this$0:Lcom/helpshift/support/external/DoubleMetaphone;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1323
    new-instance p1, Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->this$0:Lcom/helpshift/support/external/DoubleMetaphone;

    iget v0, v0, Lcom/helpshift/support/external/DoubleMetaphone;->maxCodeLen:I

    invoke-direct {p1, v0}, Ljava/lang/StringBuilder;-><init>(I)V

    iput-object p1, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->primary:Ljava/lang/StringBuilder;

    .line 1324
    new-instance p1, Ljava/lang/StringBuilder;

    iget-object v0, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->this$0:Lcom/helpshift/support/external/DoubleMetaphone;

    iget v0, v0, Lcom/helpshift/support/external/DoubleMetaphone;->maxCodeLen:I

    invoke-direct {p1, v0}, Ljava/lang/StringBuilder;-><init>(I)V

    iput-object p1, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->alternate:Ljava/lang/StringBuilder;

    .line 1328
    iput p2, p0, Lcom/helpshift/support/external/DoubleMetaphone$DoubleMetaphoneResult;->maxLength:I

    return-void
.end method
