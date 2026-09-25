.class public final Lcom/helpshift/faq/FaqsResponse;
.super Ljava/lang/Object;
.source "FaqsResponse.java"


# instance fields
.field public final response:Ljava/lang/Object;

.field public final statusCode:I


# direct methods
.method public constructor <init>(Ljava/lang/Object;I)V
    .locals 0

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput-object p1, p0, Lcom/helpshift/faq/FaqsResponse;->response:Ljava/lang/Object;

    .line 13
    iput p2, p0, Lcom/helpshift/faq/FaqsResponse;->statusCode:I

    return-void
.end method
