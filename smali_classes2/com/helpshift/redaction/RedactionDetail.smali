.class public Lcom/helpshift/redaction/RedactionDetail;
.super Ljava/lang/Object;
.source "RedactionDetail.java"


# instance fields
.field public final redactionState:Lcom/helpshift/redaction/RedactionState;

.field public final redactionType:Lcom/helpshift/redaction/RedactionType;

.field public final userLocalId:J


# direct methods
.method public constructor <init>(JLcom/helpshift/redaction/RedactionState;Lcom/helpshift/redaction/RedactionType;)V
    .locals 0

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 14
    iput-wide p1, p0, Lcom/helpshift/redaction/RedactionDetail;->userLocalId:J

    .line 15
    iput-object p3, p0, Lcom/helpshift/redaction/RedactionDetail;->redactionState:Lcom/helpshift/redaction/RedactionState;

    .line 16
    iput-object p4, p0, Lcom/helpshift/redaction/RedactionDetail;->redactionType:Lcom/helpshift/redaction/RedactionType;

    return-void
.end method
