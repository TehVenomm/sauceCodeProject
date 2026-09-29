.class public Lcom/helpshift/configuration/response/PeriodicReview;
.super Ljava/lang/Object;
.source "PeriodicReview.java"


# instance fields
.field public final interval:I

.field public final isEnabled:Z

.field public final type:Ljava/lang/String;


# direct methods
.method public constructor <init>(ZILjava/lang/String;)V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 9
    iput-boolean p1, p0, Lcom/helpshift/configuration/response/PeriodicReview;->isEnabled:Z

    .line 10
    iput p2, p0, Lcom/helpshift/configuration/response/PeriodicReview;->interval:I

    .line 11
    iput-object p3, p0, Lcom/helpshift/configuration/response/PeriodicReview;->type:Ljava/lang/String;

    return-void
.end method
