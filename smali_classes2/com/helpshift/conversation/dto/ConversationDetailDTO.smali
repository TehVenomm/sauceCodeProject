.class public Lcom/helpshift/conversation/dto/ConversationDetailDTO;
.super Ljava/lang/Object;
.source "ConversationDetailDTO.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/dto/ConversationDetailDTO$Type;
    }
.end annotation


# instance fields
.field public final timestamp:J

.field public final title:Ljava/lang/String;

.field public final type:I


# direct methods
.method public constructor <init>(Ljava/lang/String;JI)V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 9
    iput-object p1, p0, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->title:Ljava/lang/String;

    .line 10
    iput-wide p2, p0, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->timestamp:J

    .line 11
    iput p4, p0, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->type:I

    return-void
.end method
