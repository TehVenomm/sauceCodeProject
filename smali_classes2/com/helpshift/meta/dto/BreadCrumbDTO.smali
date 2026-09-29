.class public Lcom/helpshift/meta/dto/BreadCrumbDTO;
.super Ljava/lang/Object;
.source "BreadCrumbDTO.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field public final action:Ljava/lang/String;

.field public final dateTime:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 10
    iput-object p2, p0, Lcom/helpshift/meta/dto/BreadCrumbDTO;->dateTime:Ljava/lang/String;

    .line 11
    iput-object p1, p0, Lcom/helpshift/meta/dto/BreadCrumbDTO;->action:Ljava/lang/String;

    return-void
.end method
