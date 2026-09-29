.class public Lcom/helpshift/cif/dto/CustomIssueFieldDTO;
.super Ljava/lang/Object;
.source "CustomIssueFieldDTO.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field public final key:Ljava/lang/String;

.field public final type:Ljava/lang/String;

.field public final values:[Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/helpshift/cif/dto/CustomIssueFieldDTO;->key:Ljava/lang/String;

    .line 13
    iput-object p2, p0, Lcom/helpshift/cif/dto/CustomIssueFieldDTO;->type:Ljava/lang/String;

    .line 14
    iput-object p3, p0, Lcom/helpshift/cif/dto/CustomIssueFieldDTO;->values:[Ljava/lang/String;

    return-void
.end method
