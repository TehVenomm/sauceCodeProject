.class public interface abstract Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;
.super Ljava/lang/Object;
.source "HttpBackoff.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/poller/HttpBackoff;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "RetryPolicy"
.end annotation


# static fields
.field public static final ALWAYS:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

.field public static final FAILURE:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

.field public static final NEVER:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 44
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$1;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$1;-><init>()V

    sput-object v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->FAILURE:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    .line 52
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$2;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$2;-><init>()V

    sput-object v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->ALWAYS:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    .line 59
    new-instance v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$3;

    invoke-direct {v0}, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy$3;-><init>()V

    sput-object v0, Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;->NEVER:Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;

    return-void
.end method


# virtual methods
.method public abstract shouldRetry(I)Z
.end method
