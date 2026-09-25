.class public final enum Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
.super Ljava/lang/Enum;
.source "ViewableConversation.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/ViewableConversation;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "ConversationType"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

.field public static final enum HISTORY:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

.field public static final enum SINGLE:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 52
    new-instance v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    const-string v1, "HISTORY"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->HISTORY:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    .line 53
    new-instance v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    const-string v1, "SINGLE"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->SINGLE:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    const/4 v0, 0x2

    .line 51
    new-array v0, v0, [Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    sget-object v1, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->HISTORY:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->SINGLE:Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    aput-object v1, v0, v3

    sput-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->$VALUES:[Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 51
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
    .locals 1

    .line 51
    const-class v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;
    .locals 1

    .line 51
    sget-object v0, Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->$VALUES:[Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    invoke-virtual {v0}, [Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/conversation/activeconversation/ViewableConversation$ConversationType;

    return-object v0
.end method
