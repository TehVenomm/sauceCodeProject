.class public Lcom/helpshift/conversation/activeconversation/PaginationCursor;
.super Ljava/lang/Object;
.source "PaginationCursor.java"


# instance fields
.field public final conversationCursor:Ljava/lang/String;

.field public final messageCursor:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/PaginationCursor;->conversationCursor:Ljava/lang/String;

    .line 12
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/PaginationCursor;->messageCursor:Ljava/lang/String;

    return-void
.end method
